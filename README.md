# Good Gas

The use case of this tutorial app is to rate local community Gas Stations.

This a Xamain Forms mobile app showing how to pull data from a backend REST API and display it on a map.
It's pretty basic and easy to do.

Your app should NEVER connect directly to a database source or include data source credentials even in the app code. Data Sources should **always** be abstracted preferably through an API layer. Even within the API layer itself, code connecting to a physical data source should be defined as an interface so swapping physical data sources is trivial.

This function API Key **only** works on a single List function.

This project is started from the Visual Studio for Mac Multiplatform Tabbed Forms template under Xamarin.Forms using .NET Core.

This project will be used for a tutorial foundation and as you can see very little has been modified from the original to get it to work pulling live data from an Azure Table backend and Azure Function App API.

While I've started this example from the template, it's still a work in progress.

Enjoy.
