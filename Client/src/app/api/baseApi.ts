import {
  BaseQueryApi,
  FetchArgs,
  fetchBaseQuery,
} from "@reduxjs/toolkit/query";
import { startLoading, stopLoading } from "../layout/uiSlice";
import { toast } from "react-toastify";
import { router } from "../routes/Routes";

const CustomBaseQuery = fetchBaseQuery({
  baseUrl: "https://localhost:5000/api",
});

type ErrorResponse = { errorMessages: string[] };

export const baseQueryWithErrorHandling = async (
  args: string | FetchArgs,
  api: BaseQueryApi,
  extraOptions: object
) => {
  api.dispatch(startLoading());
  const result = await CustomBaseQuery(args, api, extraOptions);
  api.dispatch(stopLoading());

  if (result.error) {
    const errorResponse = result.error.data as ErrorResponse;

    if (errorResponse && "errorMessages" in errorResponse) {
      const { errorMessages } = errorResponse as {
        errorMessages: string[];
      };

      if (result.error.status === 500) {
        router.navigate("/server-error", { state: { error: errorMessages } });
      } else if (result.error.status === 404) {
        router.navigate("/not-found");
      } else {
        if (Array.isArray(errorMessages)) {
          errorMessages.forEach((msg) => toast.error(msg));
        } else {
          toast.error(errorMessages);
        }
      }
    } else {
      toast.error("No error messages found");
    }
  }

  return result;
};
