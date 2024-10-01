FROM nginx:1-alpine

COPY nginx.conf /etc/nginx/nginx.conf

COPY index.html /usr/share/nginx/html/

COPY vue_client/dist /usr/share/nginx/html/