import { createApi } from "@reduxjs/toolkit/query/react";

import { LoginResult, LoginRequest, User } from "./models/userTypes";
import { setUser } from "./slices/userSlices";
import baseQueryWithReauth from "./baseQuery";
import { Book, AddBookRequest, EditBookRequest } from "./models/BookTypes";
import { Guid } from "./models/BaseEntity";

export const api = createApi({
  reducerPath: "api",
  baseQuery: baseQueryWithReauth,
  endpoints: (builder) => ({
    addBook: builder.mutation<Guid, AddBookRequest>({
      query: (data) => ({
        url: "Library/createBook",
        method: "PUT",
        body: data,
      }),
    }),
    removeBook: builder.mutation<Guid, Guid>({
      query: (data) => ({
        url: `Library/removeBook?id=${data}`,
        method: "DELETE",
      }),
    }),
    editBook: builder.mutation<Book, EditBookRequest>({
      query: (data) => ({
        url: "Library/editBook",
        method: "PATCH",
        body: data,
      }),
    }),
    getAllBooks: builder.query<Book[], void>({
      query: () => ({ url: "Library/getAllBooks" }),
    }),
    getSingleBook: builder.query<Book, Guid>({
      query: (data) => ({ url: `Library/getBook?id=${data.id}` }),
    }),
    loginUser: builder.mutation<LoginResult, LoginRequest>({
      query: (data) => ({
        url: "Authentication/login",
        method: "POST",
        body: data,
      }),
      async onQueryStarted(args, { dispatch, queryFulfilled }) {
        try {
          const { data } = await queryFulfilled;
          const user: User = {
            id: data.id,
            token: data.token,
          };

          localStorage.setItem("user", data.token);
          dispatch(setUser(user));
        } catch (error) {
          if (error.error.status == 401);
        }
      },
    }),
  }),
});

export const {
  useLoginUserMutation,
  useGetSingleBookQuery,
  useAddBookMutation,
  useEditBookMutation,
  useGetAllBooksQuery,
  useRemoveBookMutation,
} = api;
