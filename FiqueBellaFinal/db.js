const sql = require("mssql");

const config = {
  user: process.env.DB_USER,
  password: process.env.DB_PASSWORD,
  server: process.env.DB_HOST,
  database: process.env.DB_NAME,
  port: parseInt(process.env.DB_PORT),
  options: {
    encrypt: true, // necessário no Railway
    trustServerCertificate: true, // para evitar problemas de certificado
  },
};

const poolPromise = new sql.ConnectionPool(config)
  .connect()
  .then(pool => {
    console.log("Conectado ao SQL Server!");
    return pool;
  })
  .catch(err => {
    console.error("Erro de conexão:", err);
  });

module.exports = { sql, poolPromise };
