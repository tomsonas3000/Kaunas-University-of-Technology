const express = require("express");
const mysql = require("mysql");
const cors = require("cors");
const bodyParser = require("body-parser");

const app = express();
app.use(cors(), bodyParser.urlencoded({ extended: true }), bodyParser.json());
app.use(express.json());

const port = "5000";
const http = require("http").createServer(app);

const db = mysql.createConnection({
  host: "localhost",
  user: "root",
  database: "tomjor4",
  dateStrings: ["DATE", "DATETIME"],
});

db.connect((err) => {
  if (err) {
    throw err;
  }
  console.log("my sql working");
});

app.get("/api/factories/add", (req, res) => {
  console.log(req.query);
  let sql1 = "SELECT MAX(factory_id) FROM factory";
  let query1 = db.query(sql1, (err, result) => {
    if (err) throw err;
    const id = result[0];
    idToDB = id["MAX(factory_id)"] + 1;
    console.log(idToDB);
    let factory = {
      factory_Id: idToDB,
      city: req.query.city,
      country: req.query.country,
      district: req.query.district,
      street: req.query.street,
      telephone_number: req.query.telephone_number,
      address_line: req.query.address_line,
      zip_code: req.query.zip_code,
    };
    let sql = "INSERT INTO factory SET ?";
    let query = db.query(sql, factory, (err, result) => {
      if (err) throw err;
      res.send(factory);
    });
  });
});

app.get("/api/factories/update", (req, res) => {
  let sql = `UPDATE factory SET city = '${req.query.city}',
   country = '${req.query.country}', 
   district = '${req.query.district}', 
   street = '${req.query.street}',
   telephone_number = '${req.query.telephone_number}',
   address_line = '${req.query.address_line}',
   zip_code = '${req.query.zip_code}'
   WHERE factory_id = ${req.query.factory_id}`;
  let query = db.query(sql, (err, result) => {
    if (err) throw err;
    res.send(req.query);
  });
});

app.get("/api/factories/getFactories", (req, res) => {
  let sql = "SELECT * FROM factory";
  let query = db.query(sql, (err, results) => {
    if (err) throw err;
    const factories = JSON.parse(JSON.stringify(results));
    res.send(factories);
  });
});

app.get("/api/factories/delete", (req, res) => {
  console.log(req.query.id);
  let sql = `DELETE FROM factory WHERE factory_id = ${req.query.id}`;
  let query = db.query(sql, (err, result) => {
    if (err) throw err;

    res.send(req.query.id);
  });
});
//---------------
app.get("/api/shoes/add", (req, res) => {
  console.log(":)))");
  console.log(req.query);
  let sql1 = "SELECT MAX(shoe_id) FROM shoe";
  let query1 = db.query(sql1, (err, result) => {
    if (err) throw err;
    const id = result[0];
    idToDB = id["MAX(shoe_id)"] + 1;
    console.log(idToDB);
    let shoe = {
      shoe_id: idToDB,
      welt: req.query.welt,
      size: req.query.size,
      weight: req.query.weight,
      midsole_price: req.query.midsole_price,
      welt_price: req.query.welt_price,
      eyelet_price: req.query.eyelet_price,
      midsole: req.query.midsole,
      eyelets: req.query.eyelets,
      type: req.query.type,
    };
    let sql = "INSERT INTO shoe SET ?";
    let query = db.query(sql, shoe, (err, result) => {
      if (err) throw err;
      res.send(shoe);
    });
  });
});

app.get("/api/shoes/update", (req, res) => {
  let sql = `UPDATE shoe SET welt = '${req.query.welt}', size = '${req.query.size}', weight = '${req.query.weight}', midsole_price = '${req.query.midsole_price}', welt_price = '${req.query.welt_price}', eyelet_price = '${req.query.eyelet_price}', midsole = '${req.query.midsole}', eyelets = '${req.query.eyelets}', type = '${req.query.type}' WHERE shoe_id = ${req.query.shoe_id}`;
  let query = db.query(sql, (err, result) => {
    if (err) throw err;
    res.send(req.query);
  });
});

app.get("/api/shoes/getShoes", (req, res) => {
  let sql = "SELECT * FROM shoe";
  let query = db.query(sql, (err, results) => {
    if (err) throw err;
    const factories = JSON.parse(JSON.stringify(results));
    res.send(factories);
  });
});

app.get("/api/shoes/delete", (req, res) => {
  let sql = `DELETE FROM shoe WHERE shoe_id = ${req.query.id}`;
  let query = db.query(sql, (err, result) => {
    if (err) throw err;

    res.send(req.query.id);
  });
});

app.get("/api/shoes/getTypes", (req, res) => {
  let sql = `SELECT * FROM  shoe_types`;
  let query = db.query(sql, (err, results) => {
    if (err) throw err;
    res.send(JSON.parse(JSON.stringify(results)));
  });
});

app.get("/api/shoes/getMidsoles", (req, res) => {
  let sql = `SELECT * FROM  midsoles`;
  let query = db.query(sql, (err, results) => {
    if (err) throw err;
    res.send(JSON.parse(JSON.stringify(results)));
  });
});

app.get("/api/shoes/getEyelets", (req, res) => {
  let sql = `SELECT * FROM  eyelet_types`;
  let query = db.query(sql, (err, results) => {
    if (err) throw err;
    res.send(JSON.parse(JSON.stringify(results)));
  });
});

//-------------------------------------------------------------
app.get("/api/laces/add", (req, res) => {
  console.log(req.query);
  let sql1 = "SELECT MAX(lace_id) FROM laces";
  let query1 = db.query(sql1, (err, result) => {
    if (err) throw err;
    const id = result[0];
    idToDB = id["MAX(lace_id)"] + 1;
    console.log(idToDB);
    let lace = {
      lace_id: idToDB,
      color: req.query.color,
      material: req.query.material,
      length: req.query.length,
      price: req.query.price,
      fk_SHOEshoe_id: req.query.fk_SHOEshoe_id,
    };
    let sql = "INSERT INTO laces SET ?";
    let query = db.query(sql, lace, (err, result) => {
      if (err) throw err;
      res.send(lace);
    });
  });
});

app.get("/api/laces/update", (req, res) => {
  console.log(req.query.fk_SHOEshoe_id);
  let sql = `UPDATE laces SET color = '${req.query.color}', material = '${req.query.material}', length = '${req.query.length}',  price = '${req.query.price}', fk_SHOEshoe_id = '${req.query.fk_SHOEshoe_id}' WHERE lace_id = ${req.query.lace_id}`;
  let query = db.query(sql, (err, result) => {
    if (err) throw err;
    res.send(req.query);
  });
});

app.get("/api/laces/getLaces", (req, res) => {
  let sql = "SELECT * FROM laces";
  let query = db.query(sql, (err, results) => {
    if (err) throw err;
    const laces = JSON.parse(JSON.stringify(results));
    res.send(laces);
  });
});

app.get("/api/laces/delete", (req, res) => {
  let sql = `DELETE FROM laces WHERE lace_id = ${req.query.id}`;
  let query = db.query(sql, (err, result) => {
    if (err) throw err;

    res.send(req.query.id);
  });
});

app.get("/api/laces/getFKShoes", (req, res) => {
  let sql = `SELECT shoe_id,welt,size FROM shoe ORDER BY shoe_id`;
  let query = db.query(sql, (err, results) => {
    if (err) throw err;
    console.log(JSON.parse(JSON.stringify(results)));
    res.send(JSON.parse(JSON.stringify(results)));
  });
});

//-------------------------------------------------------------------
app.get("/api/processes/add", (req, res) => {
  console.log(req.query);
  let process = {
    name: req.query.name,
    description: req.query.description,
    hourly_rate: req.query.hourly_rate,
  };
  let sql = "INSERT INTO process SET ?";
  let query = db.query(sql, process, (err, result) => {
    if (err) throw err;
    res.send(process);
  });
});

app.get("/api/processes/addMultiple", (req, res) => {
  processesToDB = [];
  req.query.processes.map((process, index) => {
    const record = JSON.parse(req.query.processes[index]);
    processesToDB.push(record);
  });
  processesToDB.map((process) => {
    let sql = `INSERT INTO process (name,description,hourly_rate) VALUES ('${process.name}', '${process.description}', '${process.hourly_rate}')`;
    let query = db.query(sql, (err, result) => {
      if (err) throw err;
    });
  });
});

app.get("/api/processes/update", (req, res) => {
  let sql = `UPDATE process SET name = '${req.query.name}', description = '${req.query.description}', hourly_rate = '${req.query.hourly_rate}' WHERE process_id = ${req.query.process_id}`;
  let query = db.query(sql, (err, result) => {
    if (err) throw err;
    res.send(req.query);
  });
});

app.get("/api/processes/getProcesses", (req, res) => {
  let sql = "SELECT * FROM process";
  let query = db.query(sql, (err, results) => {
    if (err) throw err;
    const processes = JSON.parse(JSON.stringify(results));
    res.send(processes);
  });
});

app.get("/api/processes/delete", (req, res) => {
  let sql = `DELETE FROM process WHERE process_id = ${req.query.id}`;
  let query = db.query(sql, (err, result) => {
    if (err) throw err;

    res.send(req.query.id);
  });
});

//---------------------------------------------
app.get("/api/workers/add", (req, res) => {
  console.log(req.query);
  let worker = {
    name: req.query.name,
    surname: req.query.surname,
    phone_number: req.query.phone_number,
    e_mail_address: req.query.e_mail_address,
    birth_date: req.query.birth_date,
    bank_account: req.query.bank_account,
    fk_FACTORYfactory_id: req.query.fk_FACTORYfactory_id,
  };
  let sql = "INSERT INTO worker SET ?";
  let query = db.query(sql, worker, (err, result) => {
    if (err) throw err;
    res.send(worker);
  });
});

app.get("/api/workers/addMultiple", (req, res) => {
  workersToDB = [];
  req.query.workers.map((worker, index) => {
    const record = JSON.parse(req.query.workers[index]);
    workersToDB.push(record);
  });
  workersToDB.map((worker) => {
    let sql = `INSERT INTO worker (name,surname,phone_number,e_mail_address,birth_date,bank_account,fk_FACTORYfactory_id) 
    VALUES ('${worker.name}', '${worker.surname}', '${worker.phone_number}', 
    '${worker.e_mail_address}', '${worker.birth_date}', '${worker.bank_account}', 
    '${worker.fk_FACTORYfactory_id}')`;
    let query = db.query(sql, (err, result) => {
      if (err) throw err;
    });
  });
});

app.get("/api/workers/update", (req, res) => {
  let sql = `UPDATE worker SET name = '${req.query.name}', surname = '${req.query.surname}', 
  phone_number = '${req.query.phone_number}',  e_mail_address = '${req.query.e_mail_address}', 
  birth_date = '${req.query.birth_date}', bank_account = '${req.query.bank_account}', 
  fk_FACTORYfactory_id = '${req.query.fk_FACTORYfactory_id}' WHERE worker_id = ${req.query.worker_id}`;
  let query = db.query(sql, (err, result) => {
    if (err) throw err;
    res.send(req.query);
  });
});

app.get("/api/workers/getWorkers", (req, res) => {
  let sql = "SELECT * FROM worker";
  let query = db.query(sql, (err, results) => {
    if (err) throw err;
    const workers = JSON.parse(JSON.stringify(results));
    res.send(workers);
  });
});

app.get("/api/workers/delete", (req, res) => {
  let sql = `DELETE FROM worker WHERE worker_id = ${req.query.id}`;
  let query = db.query(sql, (err, result) => {
    if (err) throw err;

    res.send(req.query.id);
  });
});

app.get("/api/workers/getFKFactories", (req, res) => {
  let sql = `SELECT factory_id,city,street FROM factory ORDER BY factory_id`;
  let query = db.query(sql, (err, results) => {
    if (err) throw err;
    console.log(JSON.parse(JSON.stringify(results)));
    res.send(JSON.parse(JSON.stringify(results)));
  });
});

app.get("/api/getReport", (req, res) => {
  let sql = `SELECT factory.factory_id, CONCAT(factory.city, " ", factory.street, " ", factory.address_line) as factory_location, 
  COUNT(IF(\`order\`.\`price\` > ${req.query.orderPrice}, 1, NULL)) as order_price_over_number, ROUND(AVG(\`order\`.\`price\`), 2) as order_average_price 
  FROM \`order\` INNER JOIN factory ON \`order\`.\`fk_FACTORYfactory_id\` = factory.factory_id 
  GROUP BY factory_id ORDER BY order_price_over_number ASC`;
  let query = db.query(sql, (err, results) => {
    if (err) throw err;
    console.log(JSON.parse(JSON.stringify(results)));
    res.send(JSON.parse(JSON.stringify(results)));
  });
});

http.listen(port, () => {
  console.log(`listening on ${port}`);
});

const router = express.Router();

/**
 * Configure the middleware.
 * bodyParser.json() returns a function that is passed as a param to app.use() as middleware
 * With the help of this method, we can now send JSON to our express application.
 */
app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());

// We export the router so that the server.js file can pick it up
module.exports = router;

// Combine react and node js servers while deploying( YOU MIGHT HAVE ALREADY DONE THIS BEFORE
// What you need to do is make the build directory on the heroku, which will contain the index.html of your react app and then point the HTTP request to the client/build directory
