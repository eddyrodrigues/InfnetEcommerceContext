import { Menubar, MenubarMenu, MenubarTrigger } from "@/components/ui/menubar";
import { Badge } from "@/components/ui/badge";
import useCartStore from "@/store/usecartStore";

export default function Header() {
  const productInCart = useCartStore((s) => s.cartProductsCount);

  return (
    <Menubar className="h-30">
      <MenubarMenu>
        <div className="flex items-center w-full">
          <div className="flex items-center gap-2">
            <MenubarTrigger className="p-2">Home</MenubarTrigger>
          </div>
          <div className="ml-auto flex items-center gap-2">
            <MenubarTrigger>
              <svg
                className="text-gray-800 dark:text-white h-[24px] w-[24px]"
                aria-hidden="true"
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
              >
                <path
                  stroke="currentColor"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth="2"
                  d="M5 4h1.5L9 16m0 0h8m-8 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4Zm8 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4Zm-8.5-3h9.25L19 7H7.312"
                />
              </svg>
              <Badge className="justify-center w-3 h-3 mt-5 -ml-2 font-semibold rounded-full p-2 m-0">
                {productInCart}
              </Badge>
            </MenubarTrigger>
          </div>
        </div>
      </MenubarMenu>
    </Menubar>
  );
}
