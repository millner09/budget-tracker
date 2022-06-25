package main

import "net/http"

func (app *application) routes() *http.ServeMux {
	mux := http.NewServeMux()

	fileServer := http.FileServer(http.Dir(app.config.staticDir))

	mux.Handle("/static/", http.StripPrefix("/static", fileServer))

	// Register the other application routes as normal.
	mux.HandleFunc("/", app.home)
	mux.HandleFunc("/hello", app.hello)
	mux.HandleFunc("/createMonthlyBudget", app.createMonthlyBudget)
	mux.HandleFunc("/viewMonthlyBudget", app.viewMonthlyBudget)
	mux.HandleFunc("/category/view", app.viewCategory)

	return mux
}
