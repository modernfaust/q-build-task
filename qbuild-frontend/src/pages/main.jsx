import React, { useEffect, useState } from "react";
import TreeView from "../components/TreeView";
import DataGrid from "../components/DataGrid";

const Main = () => {
  const [dataState, setDataState] = useState([]);

  /*   useEffect(() => {
    fetchData();
  }, []); */

  const fetchData = async () => {
    try {
      const response = await fetch(
        "http://localhost:5079/parts/parent/indentedbomview"
      );
      const data = await response.json();
      setDataState(data);
    } catch (error) {
      console.error("Error:", error);
    }
  };

  const loadData = async () => {
    if (dataState.length === 0) {
      return await fetchData();
    }
  };

  const resetData = () => {
    setDataState([]);
  };

  return (
    <div>
      <h1 className="w-full font-bold text-black text-center"> Testing Functionality for Tree and Datagrid</h1>
      <div className="width-40 flex flex-row relative">
        <TreeView data={dataState} />
        <button
          className={`absolute right-[500px] top-[150px] border-[1px] border-black rounded-md w-[200px] ${
            dataState.length !== 0 ? "bg-gray-200" : "bg-white"
          }`}
          onClick={loadData}
          disabled={dataState.length > 0}
        >
          Populate Data in Tree
        </button>
        <button
          className={`absolute right-[500px] top-[200px] border-[1px] border-black rounded-md w-[200px] bg-white`}
          onClick={resetData}
        >
          Reset Data
        </button>
      </div>
    </div>
  );
};

export default Main;
