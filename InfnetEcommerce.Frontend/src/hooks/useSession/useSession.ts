import useLoginStore from "@/store/useLoginStore";

function useSession() {
  return useLoginStore();
}

export default useSession;
