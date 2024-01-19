# UniProject Overview

## Application Layers

### Repository Layer
Handles data access and database communication.
- **AppDbContext**: Configures the database context.
- **UnitOfWork**: Implements the Unit of Work pattern.
- **Models**: Represent database tables (User, Post, Comment).
- **Repositories**: Provide methods for data querying and saving.

### Services Layer
Contains business logic.
- **UserService**: Logic for user operations.
- **PostService**: Logic for post operations.
- **CommentService**: Logic for comment operations.

### Controller Layer
Manages user requests and responses.
- **UserController**: Handles user-related HTTP requests.
- **PostController**: Handles post-related HTTP requests.
- **CommentController**: Handles comment-related HTTP requests.

### Views
Presents data to the user.
- **.cshtml files**: UI components for displaying data.

## Database Structure
- Centered around `User`, `Post`, and `Comment` entities.
- Relationships:
  - `Post` has many `Comments`.
  - `Comments` can have hierarchical replies.
  - `Posts` and `Comments` are associated with `User`.

## Current Status
- **Note**: Currently, only the User part of the application is functional.
