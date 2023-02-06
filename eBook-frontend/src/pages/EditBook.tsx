/* eslint-disable @typescript-eslint/no-unsafe-member-access */
/* eslint-disable @typescript-eslint/no-unsafe-argument */
/* eslint-disable react-hooks/exhaustive-deps */
import { Button, Container, TextField } from "@mui/material";
import { useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { useFormik } from "formik";

import { useEditBookMutation, useGetSingleBookQuery } from "../redux/api";
import type { Guid } from "../redux/models/BaseEntity";
import type { Book } from "../redux/models/BookTypes";

const EditBook = (): JSX.Element => {
  const params = useParams();
  const id = params.id as string;

  const GuidId: Guid = { id: id as string };

  const { data: book } = useGetSingleBookQuery(GuidId, {
    pollingInterval: 3000,
    refetchOnMountOrArgChange: false,
    skip: false,
  });

  const [editBook, { isError, error, isSuccess }] = useEditBookMutation();

  const navigate = useNavigate();

  useEffect(() => {
    if (error) {
      toast.error(error.data?.errors[0].messages[0]);
    }
  }, [isError, error]);

  useEffect(() => {
    if (isSuccess) {
      toast.info("Вы успешно обновили книгу");
      navigate(`/book/${id}`);
    }
  }, [isSuccess, navigate]);

  const formik = useFormik({
    initialValues: book as Book,
    onSubmit: (values) => {
      editBook(values);
    },
  });

  return (
    <>
      {book && (
        <>
          <Container className="!mx-auto !w-3/4 mt-5">
            <p className="mb-5 font-semibold">Создание книги: </p>
            <form
              onSubmit={formik.handleSubmit}
              className="flex flex-col gap-3 mb-5"
            >
              <TextField
                fullWidth
                id="name"
                name="name"
                label="Название книги"
                value={formik.values.name}
                onChange={formik.handleChange}
              />

              <TextField
                fullWidth
                id="year"
                type="number"
                name="year"
                label="Год создания книги"
                value={formik.values.year}
                onChange={formik.handleChange}
              />

              <TextField
                fullWidth
                id="information"
                name="information"
                label="Развёрнутая ифнормация о книге"
                multiline
                rows={5}
                value={formik.values.information}
                onChange={formik.handleChange}
              />

              <TextField
                fullWidth
                id="genre"
                name="genre"
                label="Жанр книги"
                value={formik.values.genre}
                onChange={formik.handleChange}
              />

              <TextField
                fullWidth
                id="imageUrl"
                name="imageUrl"
                label="Ссылка на постер книги"
                placeholder="https://..."
                value={formik.values.imageUrl}
                onChange={formik.handleChange}
              />

              <TextField
                fullWidth
                id="raiting"
                name="raiting"
                type="number"
                label="Рейтинг книги"
                value={formik.values.raiting}
                onChange={formik.handleChange}
              />

              <Button
                color="primary"
                variant="contained"
                fullWidth
                type="submit"
              >
                Обновить
              </Button>
            </form>
          </Container>
        </>
      )}
    </>
  );
};

export default EditBook;
