import { Button, Container, TextField } from "@mui/material";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { useFormik } from "formik";

import { useAddBookMutation } from "../redux/api";

const AddBook = (): JSX.Element => {
  const [addBook, { isError, error, isSuccess }] = useAddBookMutation();

  const navigate = useNavigate();

  useEffect(() => {
    if (error) {
      toast.error(error.data.errors[0].messages[0]);
    }
  }, [isError, error]);

  useEffect(() => {
    if (isSuccess) {
      toast.info("Вы успешно создали книгу");
      navigate("/");
    }
  }, [isSuccess, navigate]);

  const formik = useFormik({
    initialValues: {
      name: "",
      year: 1200,
      information: "",
      genre: "",
      authorId: "6a5d3ef4-47c4-4958-a730-2fcc3d0f13d1",
      imageUrl: "",
      raiting: 0,
    },
    onSubmit: (values) => {
      addBook(values);
    },
  });

  return (
    <Container className="!mx-auto !w-3/4 mt-5">
      <p className="mb-5 font-semibold">Создание книги: </p>
      <form onSubmit={formik.handleSubmit} className="flex flex-col gap-3 mb-5">
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

        <Button color="primary" variant="contained" fullWidth type="submit">
          Создать
        </Button>
      </form>
    </Container>
  );
};

export default AddBook;
