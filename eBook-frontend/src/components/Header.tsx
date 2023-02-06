import React, { useState } from "react";
import CollectionsBookmarkOutlinedIcon from "@mui/icons-material/CollectionsBookmarkOutlined";
import KeyboardArrowDownRoundedIcon from "@mui/icons-material/KeyboardArrowDownRounded";
import { Avatar } from "@mui/material";
import Divider from "@mui/material/Divider";
import MenuList from "@mui/material/MenuList";
import MenuItem from "@mui/material/MenuItem";
import TurnedInNotOutlinedIcon from "@mui/icons-material/TurnedInNotOutlined";
import ReviewsOutlinedIcon from "@mui/icons-material/ReviewsOutlined";
import ExitToAppOutlinedIcon from "@mui/icons-material/ExitToAppOutlined";
import Menu from "@mui/material/Menu";
import { Link } from "react-router-dom";

const Header = (): JSX.Element => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);
  const handleClickListItem = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <>
      <div className="flex items-center bg-white  h-14 w-full z-10">
        <div className="flex items-center justify-between w-11/12 mx-auto py-2 border-b-2 border-gray-100">
          <Link className="flex gap-3" to="/">
            <CollectionsBookmarkOutlinedIcon className="text-sky-600" />
            <p className="font-semibold">eBook</p>
            <p className="text-gray-400">Сервис электронных книг</p>
          </Link>
          <div
            className="flex gap-1 items-center cursor-pointer"
            onClick={handleClickListItem}
            aria-hidden="true"
          >
            <Avatar className="max-h-[30px] max-w-[30px] !bg-sky-600">P</Avatar>
            <KeyboardArrowDownRoundedIcon className="text-slate-500" />
          </div>

          <Menu
            sx={{ width: 320, marginTop: 1 }}
            anchorEl={anchorEl}
            open={open}
            onClose={handleClose}
          >
            <MenuList dense>
              <MenuItem className="flex gap-2 hover:!text-amber-600 transition">
                <Link to="book/add">
                  <TurnedInNotOutlinedIcon className="text-gray-500" />
                  Добавить книгу
                </Link>
              </MenuItem>
              <MenuItem className="flex gap-2 transition !text-gray-200">
                <ReviewsOutlinedIcon className="text-gray-200" />
                Добавить автора
              </MenuItem>
              <Divider />
              <MenuItem className="flex gap-2">
                <ExitToAppOutlinedIcon className="text-gray-500" />
                Выход
              </MenuItem>
            </MenuList>
          </Menu>
        </div>
      </div>
    </>
  );
};

export default Header;
