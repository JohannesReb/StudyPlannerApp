/** @type {import('next').NextConfig} */
const nextConfig = {
    output: 'export',
    distDir: 'dist',
    basePath: process.env.BASE_URL,
};

export default nextConfig;
