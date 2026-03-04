import { useState } from "react";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { carApi, parkingApi } from "@/services/api";
import { CarPostModel } from "@/types/api";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogTrigger } from "@/components/ui/dialog";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { useToast } from "@/hooks/use-toast";
import { Plus, Trash2 } from "lucide-react";

export default function CarsPage() {
  const qc = useQueryClient();
  const { toast } = useToast();
  const { data: cars = [], isLoading } = useQuery({ queryKey: ["cars"], queryFn: carApi.getAll });
  const { data: parkings = [] } = useQuery({ queryKey: ["parkings"], queryFn: parkingApi.getAll });

  const [addOpen, setAddOpen] = useState(false);
  const [form, setForm] = useState<CarPostModel>({ license_num: "", owner_name: "", parkingId: 0 });

  const createMut = useMutation({
    mutationFn: (data: CarPostModel) => carApi.create(data),
    onSuccess: () => { qc.invalidateQueries({ queryKey: ["cars"] }); qc.invalidateQueries({ queryKey: ["spots"] }); setAddOpen(false); toast({ title: "Car checked in" }); },
    onError: () => toast({ title: "Failed — car may already exist", variant: "destructive" }),
  });

  const deleteMut = useMutation({
    mutationFn: (id: number) => carApi.delete(id),
    onSuccess: (data) => {
      qc.invalidateQueries({ queryKey: ["cars"] });
      qc.invalidateQueries({ queryKey: ["spots"] });
      toast({ title: "Car checked out", description: data ? `Payment: ${data.paymentDue}` : undefined });
    },
    onError: () => toast({ title: "Failed to check out", variant: "destructive" }),
  });

  const formatTime = (t: string) => {
    if (!t || t === "0001-01-01T00:00:00") return "—";
    return new Date(t).toLocaleString();
  };

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-3xl font-bold text-foreground">Cars</h1>
          <p className="text-muted-foreground mt-1">Manage parked vehicles</p>
        </div>
        <Dialog open={addOpen} onOpenChange={setAddOpen}>
          <DialogTrigger asChild>
            <Button className="gradient-primary text-primary-foreground gap-2">
              <Plus className="w-4 h-4" /> Check In
            </Button>
          </DialogTrigger>
          <DialogContent>
            <DialogHeader><DialogTitle>Check In Car</DialogTitle></DialogHeader>
            <form onSubmit={e => { e.preventDefault(); createMut.mutate(form); }} className="space-y-4">
              <div><Label>License Number</Label><Input value={form.license_num} onChange={e => setForm({ ...form, license_num: e.target.value })} required className="mono" /></div>
              <div><Label>Owner Name</Label><Input value={form.owner_name} onChange={e => setForm({ ...form, owner_name: e.target.value })} required /></div>
              <div>
                <Label>Parking</Label>
                <Select onValueChange={v => setForm({ ...form, parkingId: +v })} required>
                  <SelectTrigger><SelectValue placeholder="Select parking" /></SelectTrigger>
                  <SelectContent>
                    {parkings.filter(p => p.available_spots > 0).map(p => (
                      <SelectItem key={p.id} value={String(p.id)}>{p.name} ({p.available_spots} available)</SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </div>
              <Button type="submit" className="w-full gradient-primary text-primary-foreground" disabled={createMut.isPending}>
                {createMut.isPending ? "Checking in..." : "Check In"}
              </Button>
            </form>
          </DialogContent>
        </Dialog>
      </div>

      <div className="bg-card border border-border rounded-2xl shadow-card overflow-hidden">
        {isLoading ? (
          <div className="p-12 text-center text-muted-foreground">Loading...</div>
        ) : cars.length === 0 ? (
          <div className="p-12 text-center text-muted-foreground">No cars currently parked.</div>
        ) : (
          <div className="overflow-x-auto">
            <table className="w-full">
              <thead>
                <tr className="border-b border-border bg-muted/50">
                  <th className="text-left px-6 py-3 text-sm font-medium text-muted-foreground">ID</th>
                  <th className="text-left px-6 py-3 text-sm font-medium text-muted-foreground">License</th>
                  <th className="text-left px-6 py-3 text-sm font-medium text-muted-foreground">Owner</th>
                  <th className="text-left px-6 py-3 text-sm font-medium text-muted-foreground">Entry Time</th>
                  <th className="text-left px-6 py-3 text-sm font-medium text-muted-foreground">Payment</th>
                  <th className="text-right px-6 py-3 text-sm font-medium text-muted-foreground">Actions</th>
                </tr>
              </thead>
              <tbody>
                {cars.map(c => (
                  <tr key={c.id} className="border-b border-border last:border-0 hover:bg-muted/30 transition-colors">
                    <td className="px-6 py-4 mono text-muted-foreground">{c.id}</td>
                    <td className="px-6 py-4 mono font-medium text-card-foreground">{c.license_num}</td>
                    <td className="px-6 py-4 text-card-foreground">{c.owner_name}</td>
                    <td className="px-6 py-4 text-muted-foreground text-sm">{formatTime(c.entry_time)}</td>
                    <td className="px-6 py-4 mono text-card-foreground">{c.total_payment > 0 ? `₪${c.total_payment.toFixed(2)}` : "—"}</td>
                    <td className="px-6 py-4 text-right">
                      <Button variant="ghost" size="sm" className="text-destructive gap-1"
                        onClick={() => { if (confirm("Check out this car?")) deleteMut.mutate(c.id); }}>
                        <Trash2 className="w-4 h-4" /> Check Out
                      </Button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>
    </div>
  );
}
