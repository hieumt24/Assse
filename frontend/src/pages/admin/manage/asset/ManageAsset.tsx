import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button } from "@/components/ui/button";
import { ColumnDef } from "@tanstack/react-table";
import { AssetTable } from "../../../../components/tables/asset/AssetTable";
import { PaginationState } from "@/models"; // Adjust the import path according to your project structure

export const ManageAsset = () => {
  const navigate = useNavigate();

  // Mock Data
  const mockData = [
    { id: '1', assetCode: 'LA100001', assetName: 'Laptop HP Probook 450 G1', category: 'Laptop', state: 'Available' },
    { id: '2', assetCode: 'LA100002', assetName: 'Laptop HP Probook 450 G1', category: 'Laptop', state: 'Available' },
    { id: '1', assetCode: 'LA100001', assetName: 'Laptop HP Probook 450 G1', category: 'Laptop', state: 'Available' },
    { id: '2', assetCode: 'LA100002', assetName: 'Laptop HP Probook 450 G1', category: 'Laptop', state: 'Available' },
    { id: '1', assetCode: 'LA100001', assetName: 'Laptop HP Probook 450 G1', category: 'Laptop', state: 'Available' },
    { id: '2', assetCode: 'LA100002', assetName: 'Laptop HP Probook 450 G1', category: 'Laptop', state: 'Available' },
    { id: '1', assetCode: 'LA100001', assetName: 'Laptop HP Probook 450 G1', category: 'Laptop', state: 'Available' },
    { id: '2', assetCode: 'LA100002', assetName: 'Laptop HP Probook 450 G1', category: 'Laptop', state: 'Available' },
    { id: '1', assetCode: 'LA100001', assetName: 'Laptop HP', category: 'Laptop', state: 'Available' },
    { id: '2', assetCode: 'LA100002', assetName: 'Laptop HP', category: 'Laptop', state: 'Available' },
    { id: '1', assetCode: 'LA100001', assetName: 'Laptop HP', category: 'Laptop', state: 'Available' },
    { id: '2', assetCode: 'LA100002', assetName: 'Laptop HP', category: 'Laptop', state: 'Available' },
    { id: '1', assetCode: 'LA100001', assetName: 'Laptop HP', category: 'Laptop', state: 'Available' },
    { id: '2', assetCode: 'LA100002', assetName: 'Laptop HP', category: 'Laptop', state: 'Available' },
  ];

  // Columns Definition
  const columns: ColumnDef<typeof mockData[0], any>[] = [
    { accessorKey: 'assetCode', header: 'Asset Code' },
    { accessorKey: 'assetName', header: 'Asset Name' },
    { accessorKey: 'category', header: 'Category' },
    { accessorKey: 'state', header: 'State' },
  ];

  const [pagination, setPagination] = useState<PaginationState>({
    pageIndex: 0,
    pageSize: 10,
  });

  return (
    <div className="m-24 flex h-full flex-grow flex-col gap-8">
      <p className="text-2xl font-bold text-red-600">Asset List</p>
      <div className="flex items-center justify-between">
        <Button
          variant={"destructive"}
          onClick={() => navigate("/admin/asset/create-asset")}
        >
          <span className="capitalize">Create new asset</span>
        </Button>
      </div>
      <AssetTable
        columns={columns}
        data={mockData}
        pagination={pagination}
        onPaginationChange={setPagination}
        pageCount={Math.ceil(mockData.length / pagination.pageSize)}
      />
    </div>
  );
};
