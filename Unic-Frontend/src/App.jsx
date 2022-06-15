import "./App.css";

import { useEffect, useState } from "react";

const url = "https://localhost:5001/Pessoa";
let pessoas = [];
//teste
function App() {
  const [pessoa, setPessoa] = useState([]);

  useEffect(() => {
    async function fetchData() {
      const res = await fetch(url);
      const data = await res.json();

      setPessoa(data);

      data.forEach(element => {
        pessoas.push(element);
      });

    }

    fetchData();
  }, []);
  return (
  <div className="App"> 
      
   </div>
  );
}

export default App;
