import React, { useState, useEffect } from "react";

const DataGrid = ({ selectedNode, data }) => {
  const [selectedNodeState, setSelectedNodeState] = useState(selectedNode);
  const [componentDataState, setComponentDataState] = useState(null);

  useEffect(() => {
    if (data.length === 0) {
      setComponentDataState([]);
    }
  }, [data]);

  useEffect(() => {
    setSelectedNodeState(selectedNode);
    getDetailsByComponentName(selectedNode);
  }, [selectedNode]);

  const getDetailsByComponentName = async (componentName) => {
    try {
      const response = await fetch(
        `http://localhost:5079/parts/parent/${componentName}/component/`
      );
      const data = await response.json();
      console.log(data);
      setComponentDataState(data);
    } catch (error) {
      console.error("Error:", error);
    }
  };

  return (
    <div className="w-full h-50 bg-green-500 ">
      {componentDataState ? (
        <table className="w-full border-[1px] border-black">
          <thead className="flex flex-col border-black">
            <tr className="flex flex-row font-bold">
              <td className="w-40 border-[1px] border-black">PARENT_NAME</td>
              <td className="w-40 border-[1px] border-black">COMPONENT_NAME</td>
              <td className="w-40 border-[1px] border-black">PART_NUMBER</td>
              <td className="w-40 border-[1px] border-black">TITLE</td>
              <td className="w-40 border-[1px] border-black">QUANTITY</td>
              <td className="w-40 border-[1px] border-black">TYPE</td>
              <td className="w-40 border-[1px] border-black">ITEM</td>
              <td className="w-40 border-[1px] border-black">MATERIAL</td>
            </tr>
          </thead>
          <tbody className="flex flex-col border-black">
            {componentDataState.map((item, index) => (
              <tr key={index} className="flex flex-row">
                {/* Replace these cells with your actual data values */}
                <td className="w-40 border-[1px] border-black">
                  {item.parenT_NAME}
                </td>
                <td className="w-40 border-[1px] border-black">{item.name}</td>
                <td className="w-40 border-[1px] border-black">
                  {item.parT_NUMBER}
                </td>
                <td className="w-40 border-[1px] border-black">{item.title}</td>
                <td className="w-40 border-[1px] border-black">
                  {item.quantity}
                </td>
                <td className="w-40 border-[1px] border-black">{item.type}</td>
                <td className="w-40 border-[1px] border-black">{item.item}</td>
                <td className="w-40 border-[1px] border-black">
                  {item.material}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      ) : (
        "Loading..."
      )}
    </div>
  );
};

export default DataGrid;
