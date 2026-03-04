import { useState } from "react";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { parkingApi } from "@/services/api";
import { ParkingPostModel, ParkingPutModel } from "@/types/api";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogTrigger } from "@/components/ui/dialog";
import { useToast } from "@/hooks/use-toast";
import { Plus, Pencil, Trash2 } from "lucide-react";

export default function ParkingsPage() {
  const qc = useQueryClient();
  const { toast } = useToast();
  const { data: parkings = [], isLoading } = useQuery({ queryKey: ["parkings"], queryFn: parkingApi.getAll });

  const [addOpen, setAddOpen] = useState(false);
  const [editOpen, setEditOpen] = useState(false);
  const [form, setForm] = useState({ name: "", location: "", total_spots: 0, price_per_hour: 0 });
  const [editForm, setEditForm] = useState({ id: 0, name: "", price_per_hour: 0, total_spots: 0 });

  const createMut = useMutation({
    mutationFn: (data: ParkingPostModel) => parkingApi.create(data),
    onSuccess: () => { qc.invalidateQueries({ queryKey: ["parkings"] }); setAddOpen(false); toast({ title: "Parking created" }); },
    onError: () => toast({ title: "Failed to create parking", variant: "destructive" }),
  });

  const updateMut = useMutation({
    mutationFn: ({ id, data }: { id: number; data: ParkingPutModel }) => parkingApi.update(id, data),
    onSuccess: () => { qc.invalidateQueries({ queryKey: ["parkings"] }); setEditOpen(false); toast({ title: "Parking updated" }); },
    onError: () => toast({ title: "Failed to update", variant: "destructive" }),
  });

  const deleteMut = useMutation({
    mutationFn: (id: number) => parkingApi.delete(id),
    onSuccess: () => { qc.invalidateQueries({ queryKey: ["parkings"] }); toast({ title: "Parking deleted" }); },
    onError: () => toast({ title: "Failed to delete", variant: "destructive" }),
  });

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-3xl font-bold text-foreground">Parkings</h1>
          <p className="text-muted-foreground mt-1">Manage parking locations</p>
        </div>
        <Dialog open={addOpen} onOpenChange={setAddOpen}>
          <DialogTrigger asChild>
            <Button className="gradient-primary text-primary-foreground gap-2">
              <Plus className="w-4 h-4" /> Add Parking
            </Button>
          </DialogTrigger>
          <DialogContent>
            <DialogHeader><DialogTitle>Add Parking</DialogTitle></DialogHeader>
            <form onSubmit={e => { e.preventDefault(); createMut.mutate(form); }} className="space-y-4">
              <div><Label>Name</Label><Input value={form.name} onChange={e => setForm({ ...form, name: e.target.value })} required /></div>
              <div><Label>Location</Label><Input value={form.location} onChange={e => setForm({ ...form, location: e.target.value })} required /></div>
              <div><Label>Total Spots</Label><Input type="number" value={form.total_spots} onChange={e => setForm({ ...form, total_spots: +e.target.value })} required min={1} /></div>
              <div><Label>Price per Hour (₪)</Label><Input type="number" step="0.01" value={form.price_per_hour} onChange={e => setForm({ ...form, price_per_hour: +e.target.value })} required min={0} /></div>
              <Button type="submit" className="w-full gradient-primary text-primary-foreground" disabled={createMut.isPending}>
                {createMut.isPending ? "Creating..." : "Create"}
              </Button>
            </form>
          </DialogContent>
        </Dialog>
      </div>

      <div className="bg-card border border-border rounded-2xl shadow-card overflow-hidden">
        {isLoading ? (
          <div className="p-12 text-center text-muted-foreground">Loading...</div>
        ) : parkings.length === 0 ? (
          <div className="p-12 text-center text-muted-foreground">No parkings found. Add one to get started.</div>
        ) : (
          <div className="overflow-x-auto">
            <table className="w-full">
              <thead>
                <tr className="border-b border-border bg-muted/50">
                  <th className="text-left px-6 py-3 text-sm font-medium text-muted-foreground">ID</th>
                  <th className="text-left px-6 py-3 text-sm font-medium text-muted-foreground">Name</th>
                  <th className="text-left px-6 py-3 text-sm font-medium text-muted-foreground">Location</th>
                  <th className="text-left px-6 py-3 text-sm font-medium text-muted-foreground">Available</th>
                  <th className="text-left px-6 py-3 text-sm font-medium text-muted-foreground">Price/hr</th>
                  <th className="text-right px-6 py-3 text-sm font-medium text-muted-foreground">Actions</th>
                </tr>
              </thead>
              <tbody>
                {parkings.map(p => (
                  <tr key={p.id} className="border-b border-border last:border-0 hover:bg-muted/30 transition-colors">
                    <td className="px-6 py-4 mono text-muted-foreground">{p.id}</td>
                    <td className="px-6 py-4 font-medium text-card-foreground">{p.name}</td>
                    <td className="px-6 py-4 text-muted-foreground">{p.location}</td>
                    <td className="px-6 py-4">
                      <span className={`inline-flex items-center px-2.5 py-1 rounded-full text-xs font-medium
                        ${p.available_spots > 0 ? "bg-success/10 text-success" : "bg-destructive/10 text-destructive"}`}>
                        {p.available_spots} spots
                      </span>
                    </td>
                    <td className="px-6 py-4 mono text-card-foreground">₪{p.price_per_hour}</td>
                    <td className="px-6 py-4 text-right space-x-2">
                      <Button variant="ghost" size="icon"
                        onClick={() => { setEditForm({ id: p.id, name: p.name, price_per_hour: p.price_per_hour, total_spots: 0 }); setEditOpen(true); }}>
                        <Pencil className="w-4 h-4" />
                      </Button>
                      <Button variant="ghost" size="icon" onClick={() => { if (confirm("Delete this parking?")) deleteMut.mutate(p.id); }}>
                        <Trash2 className="w-4 h-4 text-destructive" />
                      </Button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>

      {/* Edit Dialog */}
      <Dialog open={editOpen} onOpenChange={setEditOpen}>
        <DialogContent>
          <DialogHeader><DialogTitle>Edit Parking</DialogTitle></DialogHeader>
          <form onSubmit={e => { e.preventDefault(); updateMut.mutate({ id: editForm.id, data: editForm }); }} className="space-y-4">
            <div><Label>Name</Label><Input value={editForm.name} onChange={e => setEditForm({ ...editForm, name: e.target.value })} /></div>
            <div><Label>Price per Hour (₪)</Label><Input type="number" step="0.01" value={editForm.price_per_hour} onChange={e => setEditForm({ ...editForm, price_per_hour: +e.target.value })} /></div>
            <Button type="submit" className="w-full gradient-primary text-primary-foreground" disabled={updateMut.isPending}>
              {updateMut.isPending ? "Saving..." : "Save Changes"}
            </Button>
          </form>
        </DialogContent>
      </Dialog>
    </div>
  );
}
