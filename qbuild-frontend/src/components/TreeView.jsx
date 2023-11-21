import React, { useEffect, useState } from "react";
import DataGrid from "./DataGrid";

const TreeView = ({ data }) => {
  const [selectedNode, setSelectedNode] = useState(null);
  const [dataState, setDataState] = useState(data);

    useEffect(() => {
        setDataState(data);
    }, [data]);

  //console.log(selectedNode);

  return (
    <div className="w-full">
      <ul className="list-disc h-[500px] overflow-y-scroll text-black">
        {data.map((node) => (
          <TreeNode node={node} setSelectedNode={setSelectedNode} />
        ))}
      </ul>
      <DataGrid selectedNode={selectedNode} data={dataState}/>
    </div>
  );
};

const TreeNode = ({ node,setSelectedNode }) => {
  const [isExpanded, setIsExpanded] = useState(false);

  const handleToggle = (e) => {
    e.preventDefault();
    setIsExpanded(!isExpanded);
    setSelectedNode(node.name);
  };

  return (
    <li className={`mb-1 pl-2 py-1 border-l-2 border-blue-400`}>
      <div className={`flex items-center `} onClick={handleToggle}>
        <span className="mr-1 font-bold text-sm">{node.name}</span>
      </div>
      {isExpanded && node.children && (
        <ul className="ml-4">
          {node.children.map((childNode) => (
            <TreeNode node={childNode} setSelectedNode={setSelectedNode}/>
          ))}
        </ul>
      )}
    </li>
  );
};

export default TreeView;
