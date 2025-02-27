import useLoginStore from "@/store/useLoginStore";
import { ReactNode, useEffect, useState } from "react";
import { Navigate, To, useNavigate } from "react-router-dom";

type Props = {
  children: ReactNode;
};

export default function PrivateRoute({ children }: Props) {
  const loginStore = useLoginStore();
  const nav = useNavigate();
  useEffect(() => {
    if (!loginStore.RetrieveUser()) {
      loginStore.Logout();
      nav("/login");
      return;
    }
  }, []);
  return children;
}
