import AutoStoriesOutlinedIcon from "@mui/icons-material/AutoStoriesOutlined";
import StarPurple500OutlinedIcon from "@mui/icons-material/StarPurple500Outlined";
import { Link } from "react-router-dom";

import { useGetAllBooksQuery } from "../redux/api";

const Index = (): JSX.Element => {
  const { data: books } = useGetAllBooksQuery({} as void, {
    pollingInterval: 3000,
    refetchOnMountOrArgChange: false,
    skip: false,
  });

  return (
    <>
      <p className="font-semibold mt-2">Список доступных книг:</p>
      <div className="grid grid-cols-6 mt-4 gap-4">
        {books?.map((book) => (
          <Link
            className="w-full relative"
            key={book.id}
            to={`/book/${book.id}`}
          >
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
          </Link>
        ))}
      </div>
    </>
  );
};

export default Index;
