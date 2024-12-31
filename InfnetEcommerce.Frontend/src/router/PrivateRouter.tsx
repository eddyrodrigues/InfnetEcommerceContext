import useLoginStore from "@/store/useLoginStore";
import { ReactNode, useEffect, useState } from "react";
import { Navigate } from "react-router-dom";

type Props = {
  children: ReactNode;
};

export default function PrivateRoute({ children }: Props) {
  const loginStore = useLoginStore();
  const [shouldRedirect, setShouldRedirect] = useState(false);
  useEffect(() => {
    if (!loginStore.RetrieveUser()) {
      // Redirect them to the /login page, but save the current location they were
      // trying to go to when they were redirected. This allows us to send them
      // along to that page after they login, which is a nicer user experience
      // than dropping them off on the home page.
      setShouldRedirect(true);
    }
  }, []);

  if (shouldRedirect) {
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  return children;
}
