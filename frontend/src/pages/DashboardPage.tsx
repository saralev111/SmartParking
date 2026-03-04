import { useQuery } from "@tanstack/react-query";
import { parkingApi, carApi, spotApi } from "@/services/api";
import { ParkingSquare, Car, MapPin, TrendingUp } from "lucide-react";

function StatCard({ title, value, subtitle, icon: Icon, color }: {
  title: string; value: string | number; subtitle: string;
  icon: React.ElementType; color: string;
}) {
  return (
    <div className="bg-card border border-border rounded-2xl p-6 shadow-card hover:shadow-glow transition-shadow duration-300">
      <div className="flex items-start justify-between">
        <div>
          <p className="text-sm text-muted-foreground font-medium">{title}</p>
          <p className="text-3xl font-bold mt-2 text-card-foreground">{value}</p>
          <p className="text-sm text-muted-foreground mt-1">{subtitle}</p>
        </div>
        <div className={`w-12 h-12 rounded-xl flex items-center justify-center ${color}`}>
          <Icon className="w-6 h-6" />
        </div>
      </div>
    </div>
  );
}

export default function DashboardPage() {
  const { data: parkings = [] } = useQuery({ queryKey: ["parkings"], queryFn: parkingApi.getAll });
  const { data: cars = [] } = useQuery({ queryKey: ["cars"], queryFn: carApi.getAll });
  const { data: spots = [] } = useQuery({ queryKey: ["spots"], queryFn: spotApi.getAll });

  const totalSpots = spots.length;
  const occupiedSpots = spots.filter(s => s.is_occupied).length;
  const availableSpots = totalSpots - occupiedSpots;
  const occupancyRate = totalSpots > 0 ? Math.round((occupiedSpots / totalSpots) * 100) : 0;

  return (
    <div className="space-y-8">
      <div>
        <h1 className="text-3xl font-bold text-foreground">Dashboard</h1>
        <p className="text-muted-foreground mt-1">Overview of your parking system</p>
      </div>

      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
        <StatCard
          title="Total Parkings"
          value={parkings.length}
          subtitle="Active locations"
          icon={ParkingSquare}
          color="gradient-primary text-primary-foreground"
        />
        <StatCard
          title="Active Cars"
          value={cars.length}
          subtitle="Currently parked"
          icon={Car}
          color="bg-info text-info-foreground"
        />
        <StatCard
          title="Available Spots"
          value={availableSpots}
          subtitle={`of ${totalSpots} total`}
          icon={MapPin}
          color="bg-success text-success-foreground"
        />
        <StatCard
          title="Occupancy Rate"
          value={`${occupancyRate}%`}
          subtitle="Current utilization"
          icon={TrendingUp}
          color="bg-warning text-warning-foreground"
        />
      </div>

      {/* Recent parkings */}
      <div className="bg-card border border-border rounded-2xl shadow-card overflow-hidden">
        <div className="p-6 border-b border-border">
          <h2 className="text-lg font-semibold text-card-foreground">Parking Locations</h2>
        </div>
        {parkings.length === 0 ? (
          <div className="p-12 text-center text-muted-foreground">No parking locations found</div>
        ) : (
          <div className="overflow-x-auto">
            <table className="w-full">
              <thead>
                <tr className="border-b border-border bg-muted/50">
                  <th className="text-left px-6 py-3 text-sm font-medium text-muted-foreground">Name</th>
                  <th className="text-left px-6 py-3 text-sm font-medium text-muted-foreground">Location</th>
                  <th className="text-left px-6 py-3 text-sm font-medium text-muted-foreground">Available</th>
                  <th className="text-left px-6 py-3 text-sm font-medium text-muted-foreground">Price/hr</th>
                </tr>
              </thead>
              <tbody>
                {parkings.map(p => (
                  <tr key={p.id} className="border-b border-border last:border-0 hover:bg-muted/30 transition-colors">
                    <td className="px-6 py-4 font-medium text-card-foreground">{p.name}</td>
                    <td className="px-6 py-4 text-muted-foreground">{p.location}</td>
                    <td className="px-6 py-4">
                      <span className={`inline-flex items-center px-2.5 py-1 rounded-full text-xs font-medium
                        ${p.available_spots > 0 ? "bg-success/10 text-success" : "bg-destructive/10 text-destructive"}`}>
                        {p.available_spots} spots
                      </span>
                    </td>
                    <td className="px-6 py-4 mono text-card-foreground">₪{p.price_per_hour}</td>
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
