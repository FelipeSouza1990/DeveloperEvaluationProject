ALTER USER postgres WITH PASSWORD 'root';
CREATE DATABASE developer_evaluation;
GRANT ALL PRIVILEGES ON DATABASE developer_evaluation TO postgres;
