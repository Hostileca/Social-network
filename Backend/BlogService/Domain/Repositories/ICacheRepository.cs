﻿namespace Domain.Repositories;

public interface ICacheRepository
{
    public Task SetAsync<TObject>(string key, TObject value, TimeSpan expiresIn);

    public Task<TObject> GetAsync<TObject>(string key);
}