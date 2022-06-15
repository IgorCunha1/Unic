import "./App.css";

import { useEffect, useState } from "react";

const url = "https://localhost:44305/Pessoa";

function App() {
  const [pessoa, setPessoa] = useState([]);

  useEffect(() => {
    async function fetchData() {
      const res = await fetch(url);
      const data = await res.json();

      setPessoa(data);
    }

    fetchData();
  }, []);
  return <div className="App"></div>;
}

export default App;
