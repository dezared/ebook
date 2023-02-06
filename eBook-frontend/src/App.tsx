import { Outlet } from "react-router-dom";
import { ToastContainer } from "react-toastify";

import Header from "./components/Header";
import AuthorizationProvider from "./components/AuthorizationProvider";

const App = (): JSX.Element => {
  return (
    <>
      <AuthorizationProvider>
        <>
          <ToastContainer />
          <Header />
          <div className="w-11/12 mx-auto">
            <Outlet />
          </div>
        </>
      </AuthorizationProvider>
    </>
  );
};

export default App;
