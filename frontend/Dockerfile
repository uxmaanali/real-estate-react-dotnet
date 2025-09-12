# Stage 1: Build the React application
# Use a specific, stable version of Node.js
FROM node:22-alpine AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the dependency files to leverage Docker's layer caching
COPY package*.json ./

# Install project dependencies
RUN npm install

# Copy the rest of the application source code
COPY . .

# Build the React application for production
RUN npm run build

# ---

# Stage 2: Serve the application using Nginx
# Use a lightweight Nginx image to serve the static files
FROM nginx:alpine

# Copy the built files from the 'build' stage to the Nginx serving directory
# This is the key step of a multi-stage build, resulting in a small final image
COPY --from=build /app/dist /usr/share/nginx/html

# Expose port 80 to the outside world
EXPOSE 80

# The default Nginx command will run automatically, serving the content
CMD ["nginx", "-g", "daemon off;"]