import { useQuery } from "@tanstack/react-query";
import { spotApi } from "@/services/api";
import { Car, CircleDot } from "lucide-react";

export default function SpotsPage() {
  const { data: spots = [], isLoading } = useQuery({ queryKey: ["spots"], queryFn: spotApi.getAll });

  // Group spots by parking
  const grouped = spots.reduce((acc, spot) => {
    const parkingName = spot.parking?.name || "Unknown";
    if (!acc[parkingName]) acc[parkingName] = [];
    acc[parkingName].push(spot);
    return acc;
  }, {} as Record<string, typeof spots>);

  return (
    <div className="space-y-6">
      <div>
        <h1 className="text-3xl font-bold text-foreground">Spots</h1>
        <p className="text-muted-foreground mt-1">Real-time spot availability</p>
      </div>

      {isLoading ? (
        <div className="p-12 text-center text-muted-foreground">Loading...</div>
      ) : Object.keys(grouped).length === 0 ? (
        <div className="bg-card border border-border rounded-2xl p-12 text-center text-muted-foreground shadow-card">
          No spots found
        </div>
      ) : (
        Object.entries(grouped).map(([parkingName, parkingSpots]) => (
          <div key={parkingName} className="bg-card border border-border rounded-2xl shadow-card overflow-hidden">
            <div className="p-6 border-b border-border flex items-center justify-between">
              <h2 className="text-lg font-semibold text-card-foreground">{parkingName}</h2>
              <div className="flex items-center gap-4 text-sm text-muted-foreground">
                <span className="flex items-center gap-1.5">
                  <span className="w-3 h-3 rounded-full bg-success" /> Available
                </span>
                <span className="flex items-center gap-1.5">
                  <span className="w-3 h-3 rounded-full bg-destructive" /> Occupied
                </span>
              </div>
            </div>
            <div className="p-6 grid grid-cols-3 sm:grid-cols-4 md:grid-cols-6 lg:grid-cols-8 gap-3">
              {parkingSpots
                .sort((a, b) => a.spot_number - b.spot_number)
                .map(spot => (
                  <div
                    key={spot.id}
                    className={`relative flex flex-col items-center justify-center p-4 rounded-xl border-2 transition-all duration-200
                      ${spot.is_occupied
                        ? "border-destructive/30 bg-destructive/5"
                        : "border-success/30 bg-success/5 hover:border-success/60"
                      }`}
                  >
                    {spot.is_occupied ? (
                      <Car className="w-6 h-6 text-destructive" />
                    ) : (
                      <CircleDot className="w-6 h-6 text-success" />
                    )}
                    <span className="mt-1 text-sm font-medium mono text-card-foreground">#{spot.spot_number}</span>
                  </div>
                ))}
            </div>
          </div>
        ))
      )}
    </div>
  );
}
