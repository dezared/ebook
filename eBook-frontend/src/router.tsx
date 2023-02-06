import { createBrowserRouter } from "react-router-dom";

import App from "./App";
import Index from "./pages/Index";
import Login from "./pages/Login";
import SingleBook from "./pages/SingleBook";
import AddBook from "./pages/AddBook";
import EditBook from "./pages/EditBook";

const router = createBrowserRouter([
  {
    path: "",
    element: <App />,
    children: [
      {
        path: "/",
        element: <Index />,
      },
      {
        path: "/book/:id",
        element: <SingleBook />,
      },
      {
        path: "/book/add",
        element: <AddBook />,
      },
      {
        path: "/book/edit/:id",
        element: <EditBook />,
      },
    ],
  },
  {
    path: "/login",
    element: <Login />,
  },
]);

export default router;
