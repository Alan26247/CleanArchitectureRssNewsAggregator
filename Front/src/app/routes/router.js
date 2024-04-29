import { createBrowserRouter } from "react-router-dom";

import MainPage from "../../pages/MainPage/MainPage.tsx";

const router = createBrowserRouter([
    {
        path: "/",
        element: <MainPage />,
    },
]);

export default router;