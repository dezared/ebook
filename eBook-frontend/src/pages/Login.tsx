/* eslint-disable @typescript-eslint/no-unsafe-member-access */
/* eslint-disable @typescript-eslint/no-unsafe-argument */

import { Button, Container, TextField } from "@mui/material";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { useFormik } from "formik";

import { useLoginUserMutation } from "../redux/api";

const Login = (): JSX.Element => {
  const [loginUser, { isError, error, isSuccess }] = useLoginUserMutation();

  const navigate = useNavigate();

  useEffect(() => {
    if (error) {
      toast.error(error.data.errors[0].messages[0]);
    }
  }, [isError, error]);

  useEffect(() => {
    if (isSuccess) {
      toast.info("Вы успешно авторизовались");
      navigate("/");
    }
  }, [isSuccess, navigate]);

  const formik = useFormik({
    initialValues: {
      email: "",
      password: "",
    },
    onSubmit: (values) => {
      loginUser({ ...values, isPersist: true });
    },
  });

  return (
    <Container className="!mx-auto !w-3/4 mt-5">
      <form onSubmit={formik.handleSubmit} className="flex flex-col gap-3 mb-5">
        <TextField
          fullWidth
          id="email"
          name="email"
          label="Email"
          value={formik.values.email}
          onChange={formik.handleChange}
          error={formik.touched.email && Boolean(formik.errors.email)}
          helperText={formik.touched.email && formik.errors.email}
        />

        <TextField
          fullWidth
          id="password"
          name="password"
          label="Пароль"
          value={formik.values.password}
          onChange={formik.handleChange}
          error={formik.touched.password && Boolean(formik.errors.password)}
          helperText={formik.touched.password && formik.errors.password}
        />

        <Button color="primary" variant="contained" fullWidth type="submit">
          Войти
        </Button>
      </form>
    </Container>
  );
};

export default Login;
