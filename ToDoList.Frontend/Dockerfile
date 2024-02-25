# Use the official Node.js 14 image as a base
FROM node:18.18.0 AS build-end

# Set the working directory in the container
WORKDIR /app

# Copy the package.json and package-lock.json files to the container
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the remaining application code to the container
COPY . .

# Build the Angular application
RUN npm run build --prod

# Use NGINX as a lightweight web server to serve the Angular application
FROM nginx:alpine

# Copy the built Angular application from the previous stage to NGINX's html directory
COPY --from=build-end /app/dist/to-do-list.frontend /usr/share/nginx/html

# Expose port 80
EXPOSE 80

# Start NGINX when the container starts
CMD ["nginx", "-g", "daemon off;"]
