﻿using ContactMeUp.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMeUp.Data
{
    public abstract class BaseAzureStorageService<TEntity>
        where TEntity : ITableEntity, new()
    {
        private readonly string _defaultPartitionKey = typeof(TEntity).Name;
        private bool _initialized = false;
        private bool _isReadOnly;

        protected BaseAzureStorageService(IAzureStorageConfiguration azureStorageConfiguration, IConfiguration configuration)
        {
            if (azureStorageConfiguration == null)
            {
                throw new ArgumentNullException(nameof(azureStorageConfiguration));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (string.IsNullOrEmpty(azureStorageConfiguration.ConnectionStringName))
            {
                throw new ArgumentException($"{nameof(IAzureStorageConfiguration)} does not have a valid {nameof(IAzureStorageConfiguration.ConnectionStringName)}.");
            }

            if (string.IsNullOrEmpty(azureStorageConfiguration.TableName))
            {
                throw new ArgumentException($"{nameof(IAzureStorageConfiguration)} does not have a valid {nameof(IAzureStorageConfiguration.TableName)}.");
            }

            _isReadOnly = azureStorageConfiguration.IsReadOnly;

            try
            {
                string connectionString = configuration.GetConnectionString(azureStorageConfiguration.ConnectionStringName);
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
                Table = tableClient.GetTableReference(azureStorageConfiguration.TableName);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error while retrieving the table.", e);
            }
        }

        protected CloudTable Table { get; }

        public virtual async Task<IList<TEntity>> GetAsync()
        {
            if (Table == null)
            {
                throw new InvalidOperationException($"Could not find a valid {nameof(Table)}.");
            }

            if (!_initialized && !_isReadOnly)
            {
                await Table.CreateIfNotExistsAsync();
                _initialized = true;
            }

            try
            {
                TableQuery<TEntity> query = new TableQuery<TEntity>();
                TableQuerySegment<TEntity> segment = await Table.ExecuteQuerySegmentedAsync(query, null);

                return segment.Results;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error while retrieving the entities.", e);
            }
        }

        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            if (Table == null)
            {
                throw new InvalidOperationException($"Could not find a valid {nameof(Table)}.");
            }

            if (!_initialized && !_isReadOnly)
            {
                await Table.CreateIfNotExistsAsync();
                _initialized = true;
            }

            try
            {
                TableOperation operation = TableOperation.Retrieve<TEntity>(_defaultPartitionKey, id.ToString());
                TableResult result = await Table.ExecuteAsync(operation);

                if (!result.IsSuccessStatusCode())
                {
                    throw new InvalidOperationException($"Could not add the entry ({result.HttpStatusCode}) : {result.Etag} - {result.Result}");
                }

                return (TEntity)result.Result;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error while retrieving the entity.", e);
            }
        }

        public virtual async Task<TEntity> CreateOrUpdateAsync(TEntity entity)
        {
            if (_isReadOnly)
            {
                throw new InvalidOperationException($"This Azure Storage Service is in readonly mode.");
            }

            if (Table == null)
            {
                throw new InvalidOperationException($"Could not find a valid {nameof(Table)}.");
            }

            if (string.IsNullOrEmpty(entity.PartitionKey))
            {
                entity.PartitionKey = _defaultPartitionKey;
            }

            if (string.IsNullOrEmpty(entity.RowKey))
            {
                entity.RowKey = Guid.NewGuid().ToString();
            }

            if (!_initialized)
            {
                await Table.CreateIfNotExistsAsync();
                _initialized = true;
            }

            try
            {
                TableOperation operation = TableOperation.InsertOrReplace(entity);
                TableResult result = await Table.ExecuteAsync(operation);

                if (!result.IsSuccessStatusCode())
                {
                    throw new InvalidOperationException($"Could not add the entry ({result.HttpStatusCode}) : {result.Etag} - {result.Result}");
                }

                return (TEntity)result.Result;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error while creating the entity.", e);
            }
        }

        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            if (_isReadOnly)
            {
                throw new InvalidOperationException($"This Azure Storage Service is in readonly mode.");
            }

            if (Table == null)
            {
                throw new InvalidOperationException($"Could not find a valid {nameof(Table)}.");
            }

            if (!_initialized)
            {
                throw new InvalidOperationException($"The {nameof(Table)} is not initialized.");
            }

            if (string.IsNullOrEmpty(entity.RowKey))
            {
                throw new ArgumentException($"The entity does not have a valid {nameof(ITableEntity.RowKey)}.");
            }

            try
            {
                TableOperation operation = TableOperation.Delete(entity);
                TableResult result = await Table.ExecuteAsync(operation);

                if (!result.IsSuccessStatusCode())
                {
                    throw new InvalidOperationException($"Could not delete the entry ({result.HttpStatusCode}) : {result.Etag} - {result.Result}");
                }

                return 1;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error while deleting the entity.", e);
            }
        }
    }
}