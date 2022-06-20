package main

import (
	"fmt"
	"log"
	"net/http"
	"strconv"
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

	id, err := strconv.Atoi(r.URL.Query().Get("id"))

	if err != nil || id < 1 {
		http.NotFound(w, r)
		return
	}

	fmt.Fprintf(w, "Display a specific budget with ID %d...", id)
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
