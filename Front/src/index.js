import ReactDOM from 'react-dom/client';
import router from "./app/routes/router"
import { RouterProvider } from "react-router-dom";
import { Provider } from 'react-redux';
import { store } from './app/store'

// ----- стили -----
import "./shared/assets/css/index.css";
import "./shared/assets/css/bootstrap/bootstrap.min.css";
import "./shared/assets/css/app.css";
import "./shared/assets/css/colors.css";
import "./shared/assets/css/components.css";

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <Provider store={store}>
    <RouterProvider router={router} />
  </Provider>
);
