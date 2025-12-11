# RecipeSharingAPI

A RESTful API built to manage recipes, allow chefs to register and publish their own content, and provide public access to a rich, filterable, paginated recipe feed. The project includes secure authentication, image processing, and multiple filtering features.


---

🚀 Features

## Public Recipe Listings

Retrieve a list of all recipes.

Supports multiple filters:

## Keyword search (text query)

## Publication date

## Chef

## Labels/tags


Results are paginated, with metadata returned on every page.


## Chef Registration & Authentication

Users can sign up as chefs.

JWT-secured login flow to protect all recipe-creation endpoints.

Only authenticated chefs can create and manage their own recipe posts.


## Recipe Creation with Image Processing

Chefs can upload recipes including images.

Uploaded images are automatically:

Re-sized to a standard dimension (customizable)

Processed using libraries such as Sharp


Ensures consistent image formatting across the application.
