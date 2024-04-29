import axios from "axios";

export function SetTokens(id_token, access_token, refresh_token)
{
    localStorage.setItem("id_token", id_token);
    localStorage.setItem("access_token", access_token);
    localStorage.setItem("refresh_token", refresh_token);

    axios.defaults.headers.common["Authorization"] = `Bearer ${access_token}`;
}

export function LoadAccessTokenToAxios()
{
    const access_token = localStorage.getItem("access_token");

    axios.defaults.headers.common["Authorization"] = `Bearer ${access_token}`;
}

export function GetRefreshToken()
{
    return localStorage.getItem("refresh_token");
}