package models

import (
	"database/sql"
	"errors"
	"time"
)

type Category struct {
	Id        int
	Title     string
	CreatedOn time.Time
	Enabled   bool
}

type CategoryModel struct {
	DB *sql.DB
}

func (m *CategoryModel) GetAllEnabled() ([]*Category, error) {
	stmt := `SELECT id, title, enabled, createdOn FROM categories WHERE enabled = true;`

	rows, err := m.DB.Query(stmt)
	if err != nil {
		return nil, err
	}

	defer rows.Close()

	categories := []*Category{}

	for rows.Next() {
		c := &Category{}

		err = rows.Scan(&c.Id, &c.Title, &c.Enabled, &c.CreatedOn)
		if err != nil {
			return nil, err
		}

		categories = append(categories, c)
	}

	if err = rows.Err(); err != nil {
		return nil, err
	}

	return categories, nil
}

func (m *CategoryModel) GetById(id int) (*Category, error) {
	stmt := "SELECT id, title, enabled, createdOn FROM categories WHERE enabled = true AND id= ?;"

	row := m.DB.QueryRow(stmt, id)

	c := &Category{}

	err := row.Scan(&c.Id, &c.Title, &c.Enabled, &c.CreatedOn)

	if err != nil {
		if errors.Is(err, sql.ErrNoRows) {
			return nil, ErrNoRecord
		} else {
			return nil, err
		}
	}

	return c, nil
}
