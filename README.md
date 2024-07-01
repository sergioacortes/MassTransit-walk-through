# NServiceBus-walk-through

This solution contains the projects used by the NServiceBus walk through sample.

- NServiceBusSample.Orders, this project is the responsible of post new order on the system.
- NServiceBusSample.Sales, this project is responsible of create the order and dispatch the order completed message.
- NServiceBusSample.Billing, this project is responsible of react to the order completed message to process the billing.

The objective of this walk through sample repo is to give you and simple overview of how to use / implement NServiceBus in your library, for this reason console applications are used in this solution, but feel free to change it for another kind of projects (ASP.NET Core MVC, ASP.NET Core Razor, etc.)