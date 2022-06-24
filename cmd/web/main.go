package main

import (
	"database/sql"
	"flag"
	"log"
	"net/http"
	"os"

	"github.com/millner09/budget-tracker/internal/models"

	_ "github.com/go-sql-driver/mysql" // New import
)

type config struct {
	addr      string
	staticDir string
	dsn       string
}

type application struct {
	errorLog   *log.Logger
	infoLog    *log.Logger
	config     *config
	categories *models.CategoryModel
}

func main() {
	var cfg config

	flag.StringVar(&cfg.addr, "addr", ":4000", "HTTP network address")

	flag.StringVar(&cfg.staticDir, "static-dir", "./ui/static/", "Path to static assets")
	flag.StringVar(&cfg.dsn, "dsn", "", "MySQL data source name")

	flag.Parse()

	infoLog := log.New(os.Stdout, "INFO\t", log.Ldate|log.Ltime)
	errorLog := log.New(os.Stderr, "ERROR\t", log.Ldate|log.Ltime|log.Lshortfile)

	db, err := openDB(cfg.dsn)
	if err != nil {
		errorLog.Fatal(err)
	}
	defer db.Close()

	app := &application{
		errorLog:   errorLog,
		infoLog:    infoLog,
		config:     &cfg,
		categories: &models.CategoryModel{DB: db},
	}

	srv := &http.Server{
		Addr:     cfg.addr,
		ErrorLog: errorLog,
		Handler:  app.routes(),
	}

	log.Printf("Starting server on http://localhost%s", cfg.addr)
	err = srv.ListenAndServe()
	errorLog.Fatal(err)
}

// The openDB() function wraps sql.Open() and returns a sql.DB connection pool
// for a given DSN.
func openDB(dsn string) (*sql.DB, error) {
	db, err := sql.Open("mysql", dsn)
	if err != nil {
		return nil, err
	}
	if err = db.Ping(); err != nil {
		return nil, err
	}
	return db, nil
}
