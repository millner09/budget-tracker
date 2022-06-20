package main

import (
	"log"
	"net/http"
)

func hello(w http.ResponseWriter, r *http.Request) {
	w.Write([]byte("Hello, world!"))
}

func createMonthlyBudget(w http.ResponseWriter, r *http.Request) {
	if r.Method != http.MethodPost {
		w.Header().Set("Allow", http.MethodPost)
		http.Error(w, "Method Not Allowed", http.StatusMethodNotAllowed)
		return
	}
	w.Write([]byte("Creating Monthly budget..."))
}

func viewMonthlyBudget(w http.ResponseWriter, r *http.Request) {
	if r.Method != http.MethodGet {
		w.Header().Set("Allow", http.MethodGet)
		http.Error(w, "Method Not Allowed", http.StatusMethodNotAllowed)
		return
	}
	w.Write([]byte("Viewing Monthly budget..."))
}

func main() {
	mux := http.NewServeMux()

	mux.HandleFunc("/hello", hello)
	mux.HandleFunc("/createMonthlyBudget", createMonthlyBudget)
	mux.HandleFunc("/viewMonthlyBudget", viewMonthlyBudget)

	log.Println("Starting server on :4000")
	err := http.ListenAndServe(":4000", mux)
	log.Fatal(err)
}
