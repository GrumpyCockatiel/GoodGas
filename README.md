# Good Gas

The use case of this tutorial app is to rate local community Gas Stations.

This a Xamain Forms mobile app showing how to pull data from a backend REST API and display it on a map.
It's pretty basic and easy to do.

Your app should NEVER connect directly to a database source or include data source credentials even in the app code. Data Sources should **always** be abstracted preferably through an API layer. Even within the API layer itself, code connecting to a physical data source should be defined as an interface so swapping physical data sources is trivial. For example, your app may start out pulling data from a MySQL database only to later migrate to MongoDB or Redis. This should not require any recode, only adding new physical implementation classes to the back-end API. The mobile should never know the difference.

This project is started from the Visual Studio for Mac Multiplatform Tabbed Forms template under Xamarin.Forms using .NET Core. It will work fine in Visual Studio for Windows. I tend to use macOS for development, even .NET stuff, mainly because of the tools.

This project will be used for a tutorial foundation and as you can see very little has been modified from the original to get it to work pulling live data from an Azure Table backend and Azure Function App API.

While I've started this example from the template, it's still a work in progress. There's no intention to publish this to any app store.

Enjoy.

## Getting it Working

* Install the latest version of Visual Studio for Windows or Mac. It should work on either
* On a Mac you will also want to have the latest version of XCode installed for running the iOS Simulator
* Clone or download this repo
* Open the .sln file at the root and Run the project

### iOS or Android

I'm biased, I usually give more attention to the iOS implementation. However, you can run either, though the Android version is behind right now. You will need a simulator/emulator installed or a physical device to load it to.

## Design Decisions

When architecting a new app, there are lots of things we need consider:

### Refreshing Data

How often to refresh app data is one that depends on several factors:

* How is the app used?
* How often is new data added?
* Does a user see a community data set or only their own?
* How much data is there?

A simple trick is refresh data not only when the app is launched but also when it brought to the foreground. Also, each time a view is instantiated or brought into focus we may again refresh the data. The Refresh View container controler is used to allow a user to explicitly refresh data by pulling down on the screen, but any simple UI control can do the same thing. If data is continuously being added to the app, then a push notification approach may need to be utilized.

Another approach is simply added a API call that returns the date of the last data addition. If the app's stale is older, then refresh the data.

If the total amount of data is small, then a full feed approach can be used. For large amounts of data, then a partial feed might be needed. Data can be stored locally and compared for deltas.

### Design Patterns

UI client apps can have a lot of nested hierarchy between parent and child views/controls. This can get tedious especially since most views do not get created until they are first used and some things get disposed to save on memory like table cells that pass out of view.

This can make responding to events from some deeply nested child control on one tab by some view "far away" a bit of a pain. Xamarin includes a MessagingCenter class to implement the **publish-subscribe pattern**. Sadly, it sucks. Don't use it. The listener has to know data type of the sender which just isn't realistic. You can find a 3rd party lib to better implement this but we'll make our own.

## How it Works

### UI Configuration

Remember that XAML is just a UI configuration syntax much like you find in many other UI development environments. It's just XML that tells how to instatiate code. Old school iOS had NIB files which later became .storyboard files (also XML), and so on...

When you see something like
```
ItemsSource="{Binding Items}"
```
All it's saying is that this instance of the control wants to subscribe to the event for when this property 'Items' changes in the Binding Context. The Binding Context can be any class including itself, but most often a separate ViewModel class. Such binding can be one or two ways.

## Data Store

Most every app has some data it needs to at least retrieve and display, as well possibly store locally and possibly update back to the source. We never want our client side binary package to include code or credentials to connect directly to a physical data source. Always assume a local binary can be extracted, de-compiled and perused for useful credentials. BASE64 encoding a hardcoded password isn't going to stop anyone.

In the real world developers working on the backend may be working in parallel with those doing the client. All they need to agree upon is an interface definition like this simple example:

```
string Login(string id, string pw);
bool Logout(string token);
MyObject[] ListMyObjects(string token);
```

To kick off this app the Microsoft has defined an interface called IDataStore. They've included more CRUD operations than we will use at this time. We'll start with the most obvious:

```
public interface IDataStore<T>
{
	/// <summary>Get a list of all the items</summary>
	/// <returns>A list of items of type T</returns>
	Task<IEnumerable<GasStation>> ListAll();
}
```

The generic type T is the business or domain entity that correlates to this data store. The issue with this design is that it assumes **all** your data stores will have the same operations. While getting and listing are very common, inserting and updating my be less so and deleting very rare. Of course, you can still implement these is a concrete class to do nothing - but there is such a thing as too generic. It's OK to create iterface definitions for different entities.

For starters we will have the concrete MockDataStore and GasService.

```
public class MockDataStore : IDataStore<GasStation>
```

The later will actually connect to the backend API:
```
public class GasService : RESTServiceBase, IDataStore<GasStation>
```
using a REST Service endpoint. From that point on, the client has no idea how the backend is implemented. For it knows the data is loaded from a CSV file, or comes from 3 different sources. It should not matter and a physical change the data source shouldn't break the app.

### IoC and Dependency Service

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

### Configuration Environments

Coming...

## Notes

* The function API Key **only** works on a single List function.
* Not all the images and icons have been yet migrated to the Android version.