# Neocoder
A .Net standard geocoder library that can query multiple backend geocoders and rotate between them.

Currently supports Google and Bing geocoder APIs.

## Status

[![Build status](https://dev.azure.com/ryanoneill/Neocoder/_apis/build/status/Neocoder-ASP.NET%20Core-CI)](https://dev.azure.com/ryanoneill/Neocoder/_build/latest?definitionId=2)

## Quickstart

See the Neocoder.Console project for an example of the following.
Set your Google and Bing geocoder API keys in appsettings.json.
Register Neocoder for use with .Net Core Dependency Injection via;

            // This gets the geocoder settings out of your configuration file.
            var appSettings = configuration.GetSection("App").Get<AppSettings>();

            // Register the settings and Neocoder.
            return new ServiceCollection()
                .AddSingleton(appSettings.GeocoderSettings)
                .AddGeocoders()
                .BuildServiceProvider();

In your main code

                // Normally you'd get this injected via DI.
                var geocodeManager = serviceProvider.GetRequiredService<GeocodeManager>();

                var geocoded = await geocodeManager.GeocodeAddressAsync(address);


## Running multiple instances
If using this library in a multi-process environment (such as serverless functions or a webfarm) then you'll need to implement your own Geocoder selector with the interface IGeocoderSelector. The class you implement would need to figure out what geocoder to select next based on what was used previously by accessing a shared resource (Redis, database layer etc), This would typically involve a locking fetch / update on the resource. See InMemoryGeocoderSelector.cs for ideas.

