package main

import (
	"log"
	"net/http"
)

func main() {
	mux := http.NewServeMux()

	mux.HandleFunc("/", home)
	mux.HandleFunc("/hello", hello)
	mux.HandleFunc("/createMonthlyBudget", createMonthlyBudget)
	mux.HandleFunc("/viewMonthlyBudget", viewMonthlyBudget)

	log.Println("Starting server on :4000")
	err := http.ListenAndServe(":4000", mux)
	log.Fatal(err)
}