const Header = () => {
  return (
    <header>
      <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-primary border-bottom box-shadow">
        <div className="container">
          <a className="navbar-brand  text-white" asp-area="" asp-page="/Index" href="/">Лента новостей</a>
        </div>
      </nav>
    </header>
  );
}

export default Header;