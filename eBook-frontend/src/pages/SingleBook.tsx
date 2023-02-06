import { Link, useParams, useNavigate } from "react-router-dom";
import AutoStoriesOutlinedIcon from "@mui/icons-material/AutoStoriesOutlined";
import StarPurple500OutlinedIcon from "@mui/icons-material/StarPurple500Outlined";
import Button from "@mui/material/Button/Button";
import Divider from "@mui/material/Divider";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { Dialog, DialogActions, DialogTitle } from "@mui/material";
import { useState } from "react";

import { useGetSingleBookQuery, useRemoveBookMutation } from "../redux/api";
import type { Guid } from "../redux/models/BaseEntity";

const SingleBook = (): JSX.Element => {
  const params = useParams();
  const id = params.id as string;

  const [open, setOpen] = useState(false);
  const navigate = useNavigate();

  const [removeBook] = useRemoveBookMutation();

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const handleCloseOnDelete = () => {
    removeBook({ id } as Guid);
    setOpen(false);
    toast.info("Вы успешно удалили книгу");
    navigate("/");
  };

  const GuidId: Guid = { id: id as string };

  const { data: book } = useGetSingleBookQuery(GuidId, {
    pollingInterval: 3000,
    refetchOnMountOrArgChange: false,
    skip: false,
  });

  return (
    <>
      {book && (
        <>
          <Dialog
            open={open}
            onClose={handleClose}
            aria-labelledby="alert-dialog-title"
            aria-describedby="alert-dialog-description"
          >
            <DialogTitle id="alert-dialog-title">
              Вы уверены что хотите удалить эту книгу?
            </DialogTitle>
            <DialogActions>
              <Button onClick={handleClose}>Отменить</Button>
              <Button color="error" onClick={handleCloseOnDelete}>
                Удалить
              </Button>
            </DialogActions>
          </Dialog>
          <div className="flex gap-5">
            <div className="flex-1 flex flex-col" key={book.id}>
              <p className="font-semibold mb-3">Основная информация:</p>
              <div className="w-full relative">
                <div className="z-10 h-[240px] opacity-80 hover:opacity-100 overflow-hidden rounded hover:h-min transition cursor-pointer absolute top-0 left-0">
                  <img
                    src={book.imageUrl}
                    alt="poster"
                    className="w-full object-cover"
                  />
                </div>
                <p className="text-base text-black font-semibold mt-[250px]">
                  {book.name}
                </p>
                <div className="flex gap-1 items-center">
                  <AutoStoriesOutlinedIcon className="text-gray-400 max-w-[19px] max-h-[19px]" />
                  <p>
                    Год:
                    <span>{book.year}</span>
                  </p>
                </div>
                <div className="flex gap-1 items-center">
                  <StarPurple500OutlinedIcon className="text-gray-400 max-w-[19px] max-h-[19px]" />
                  <p>
                    Рейтинг:
                    <span className="text-white bg-gradient-to-r from-green-400 to-green-600 px-1 py-[1px] rounded ml-1">
                      {book.raiting.toFixed(2)}
                    </span>
                  </p>
                </div>
              </div>
            </div>
            <div className="flex-1">
              <p className="font-semibold mb-3">Обзор:</p>
              <p className="mb-3">{book.information}</p>
              <Divider />
              <div className="flex gap-2 mt-3">
                <Link to={`../../book/edit/${id}`} relative="path">
                  <Button color="info" variant="outlined">
                    Редактировать
                  </Button>
                </Link>
                <Button
                  color="error"
                  variant="outlined"
                  onClick={handleClickOpen}
                >
                  Удалить
                </Button>
              </div>
            </div>
          </div>
        </>
      )}
    </>
  );
};

export default SingleBook;
