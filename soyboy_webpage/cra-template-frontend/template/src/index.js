import { createRoot } from "react-dom/client";
import React from "react";
import reportWebVitals from './reportWebVitals';

import {
    createBrowserRouter,
    RouterProvider,
  } from "react-router-dom";

import App from './App';
import Game from "./components/game";
import Comment from "./components/Comment"
import "./templates/index.css";

const router = createBrowserRouter([
    {
      path: "/",
      element: <App />,
    },
    {
      path: "/game/",
      element: <Game />
    },
    {
      path: "/comment/",
      element: <Comment />
    }
  ]);

const root = createRoot(document.getElementById('root'));
root.render(
    <React.StrictMode>
        <RouterProvider router={router} />
    </React.StrictMode>
)

reportWebVitals();