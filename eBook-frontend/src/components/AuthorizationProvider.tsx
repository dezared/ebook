import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";

import Login from "../pages/Login";
import { useAppSelector } from "../redux/store";
import { User } from "../redux/models/userTypes";
import { setUser } from "../redux/slices/userSlices";

interface AuthorizationProviderProps {
  children: JSX.Element;
}

const AuthorizationProvider = ({
  children,
}: AuthorizationProviderProps): JSX.Element => {
  const [isLoaded, setLoaded] = useState(false);
  const user = useAppSelector((state) => state.userState.user);
  const dispatch = useDispatch();

  useEffect(() => {
    const data = localStorage.getItem("user");
    if (data !== null) {
      const user: User = {
        token: data,
      };

      dispatch(setUser(user));
    }

    setLoaded(true);
  }, []);

  return (
    <>
      {isLoaded && user !== null && <>{children}</>}

      {isLoaded && user == null && <Login />}
    </>
  );
};

export default AuthorizationProvider;
