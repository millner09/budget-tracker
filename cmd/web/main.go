package main

import (
	"flag"
	"log"
	"net/http"
)

type config struct {
	addr      string
	staticDir string
}

func main() {
	var cfg config

	flag.StringVar(&cfg.addr, "addr", ":4000", "HTTP network address")

	flag.StringVar(&cfg.staticDir, "static-dir", "./ui/static/", "Path to static assets")

	mux := http.NewServeMux()

	// Create a file server which serves files out of the "./ui/static" directory.
	// Note that the path given to the http.Dir function is relative to the project
	// directory root.
	fileServer := http.FileServer(http.Dir(cfg.staticDir))

	// Use the mux.Handle() function to register the file server as the handler for
	// all URL paths that start with "/static/". For matching paths, we strip the
	// "/static" prefix before the request reaches the file server.
	mux.Handle("/static/", http.StripPrefix("/static", fileServer))

	// Register the other application routes as normal.
	mux.HandleFunc("/", home)
	mux.HandleFunc("/hello", hello)
	mux.HandleFunc("/createMonthlyBudget", createMonthlyBudget)
	mux.HandleFunc("/viewMonthlyBudget", viewMonthlyBudget)

	log.Printf("Starting server on http://localhost%s", cfg.addr)
	err := http.ListenAndServe(cfg.addr, mux)
	log.Fatal(err)
}
