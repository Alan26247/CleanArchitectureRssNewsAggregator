export function GetUserClaims() {

    const id_token = localStorage.getItem("id_token");

    // console.log("id_token", id_token);
    
    if(id_token === undefined || id_token === "undefined" || id_token === null) return null;

    const base64Url = id_token.split(".")[1];
    const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
    const claims = decodeURIComponent(
        atob(base64)
        .split("")
        .map(function (c) {
            return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
        })
        .join("")
    );

    // console.log("пользователь", claims);

    return JSON.parse(claims);
}