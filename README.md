# EShopMicrosrvices

Solution Overview
The EShop Microservices solution is an e-commerce platform built using a microservices architecture. It is designed to handle various aspects of an online store, such as managing baskets, product catalogs, discounts, and orders. The solution is implemented in .NET 8 and follows modern development practices like Domain-Driven Design (DDD), Event-Driven Architecture, and containerization.
---
Projects in the Solution
1. Basket Service
•	Project File: Basket.API.csproj
•	Description:
•	Manages the shopping basket for users.
•	Provides APIs for storing, retrieving, and managing basket data.
•	Key Features:
•	Handles basket operations such as adding, updating, and removing items.
•	Implements endpoints in StoreBasketEndpoints.cs.
•	Docker Support:
•	Includes a Dockerfile for containerization.
---
2. Catalog Service
•	Project File: Catalog.API.csproj
•	Description:
•	Manages the product catalog and inventory.
•	Provides data about products available in the store.
•	Key Features:
•	Seeds initial product data using CatalogInitialData.cs.
•	Supports querying product details and inventory.
•	Purpose:
•	Acts as the source of truth for product information.
---
3. Discount Service
•	Project File: Discount.Grpc.csproj
•	Description:
•	Provides discount-related operations via gRPC.
•	Handles discount calculations and management.
•	Key Features:
•	Implements gRPC services in DiscountService.cs.
•	Includes database migrations for managing discount data.
•	Purpose:
•	Enables dynamic discounting for products and orders.
---
4. Ordering Service
•	Project File: Ordering.API.csproj
•	Description:
•	Manages order processing and related operations.
•	Handles order creation, updates, and retrieval.
•	Key Features:
•	Uses DispatchDomainEventsInterceptor for domain event handling.
•	Seeds initial order data using DatabaseExtentions.cs.
•	Includes Docker support for containerization (Dockerfile).
•	Purpose:
•	Ensures reliable and consistent order management.
