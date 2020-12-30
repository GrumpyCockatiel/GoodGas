# Good Gas

The use case of this tutorial app is to rate local community Gas Stations.

This a Xamain Forms mobile app showing how to pull data from a backend REST API and display it on a map.
It's pretty basic and easy to do.

Your app should NEVER connect directly to a database source or include data source credentials even in the app code. Data Sources should **always** be abstracted preferably through an API layer. Even within the API layer itself, code connecting to a physical data source should be defined as an interface so swapping physical data sources is trivial. For example, your app may start out pulling data from a MySQL database only to later migrate to MongoDB or Redis. This should not require any recode, only adding new physical implementation classes to the back-end API. The mobile should never know the difference.

This project is started from the Visual Studio for Mac Multiplatform Tabbed Forms template under Xamarin.Forms using .NET Core. It will work fine in Visual Studio for Windows. I tend to use macOS for development, even .NET stuff, mainly because of the tools.

This project will be used for a tutorial foundation and as you can see very little has been modified from the original to get it to work pulling live data from an Azure Table backend and Azure Function App API.

While I've started this example from the template, it's still a work in progress. There's no intention to publish this to any app store.

Enjoy.

## Refreshing Data

How often to refresh app data is one that depends on several factors:

* How is the app used?
* How often is new data added?
* Does a user see a community data set or only their own?
* How much data is there?

A simple trick is refresh data not only when the app is launched but also when it brought to the foreground. Also, each time a view is instantiated or brought into focus we may again refresh the data. The Refresh View container controler is used to allow a user to explicitly refresh data by pulling down on the screen, but any simple UI control can do the same thing. If data is continuously being added to the app, then a push notification approach may need to be utilized.

Another approach is simply added a API call that returns the date of the last data addition. If the app's stale is older, then refresh the data.

If the total amount of data is small, then a full feed approach can be used. For large amounts of data, then a partial feed might be needed. Data can be stored locally and compared for deltas.

## How it Works
Remeber that XAML is just a UI configuration syntax much like you find in many other UI development environments. It's just XML that tells how to instatiate code. Old school iOS had NIB files which later became .storyboard files (also XML), and so on...

When you see something like
```
ItemsSource="{Binding Items}"
```
All it's saying is that this instance of the control wants to subscribe to the event for when this property 'Items' changes in the Binding Context. The Binding Context can be any class including itself, but most often a separate ViewModel class. Such binding can be one or two ways.

## IoC and Dependency Service

We always want to build our apps with testing in mind. A client app should be able to run even with the backend service may be down or disconnected from the Internet. You'll notice the template projects starts with a MockDataStore and created using the Xamarin Forms Dependecy Service.

```
DependencyService.Register<MockDataStore>();
```

A major issue with any cross-platform environment is being able to hardware or platform specific functions in which no bridge code has yet to be written. For example, getting location from the device GPS. In a native environment, this is trivial. But how it's done in iOS vs Android si completely different. Over time, Xamarain adds more and more bridging code to handle these functions, but it's always behind the latest release. This is where the Dependency Service can help instantiate two difference code bases depending upon the runtime environment.

But we can use it here to load up a different data source based on some config setting or logic.

The Mock Data Store doesn't need a network connection and returns hard coded data good for testing. In order to switch the API connected Data store it's one change:

```
//DependencyService.RegisterSingleton<IDataStore<GasStation>>( new MockDataStore() );
DependencyService.RegisterSingleton<IDataStore<GasStation>>( new GasService(APIBaseURL, FunctionKey) );
```

## Configuration Environments

Coming...

## Notes

* The function API Key **only** works on a single List function.
* Not all the images and icons have been yet migrated to the Android version.