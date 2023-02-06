/* eslint-disable @typescript-eslint/indent */
import { fetchBaseQuery } from "@reduxjs/toolkit/query";
import type {
  BaseQueryFn,
  FetchArgs,
  FetchBaseQueryError,
} from "@reduxjs/toolkit/query";

const baseQuery = fetchBaseQuery({ baseUrl: "http://localhost:5001/api/" });
const baseQueryWithReauth: BaseQueryFn<
  string | FetchArgs,
  unknown,
  FetchBaseQueryError
> = async (args, api, extraOptions) => {
  let result = await baseQuery(args, api, extraOptions);
  if (result.error && result.error.status === 401) {
    const refreshResult = await baseQuery("/refreshToken", api, extraOptions);
    if (refreshResult.data) {
      // console.log("token");
      result = await baseQuery(args, api, extraOptions);
    } else {
      // console.log("logout");
    }
  }
  return result;
};

export default baseQueryWithReauth;
