import { useEffect, useState } from "react";
import { IPropertyDto } from "../dtos/property/property-dto";
import { PropertyService } from "../services/property.service";
import PropertyCard from "../components/property-card";

export default function Favorites() {
	const [properties, setProperties] = useState<IPropertyDto[]>([]);
	const [loading, setLoading] = useState(false);
	const [error, setError] = useState<string | null>(null);

	const fetchProperties = async () => {
		setLoading(true);
		setError(null);
		try {
			const response = await PropertyService.GetFavoriteProperties();
			if (response.success && response.response) {
				setProperties(response.response);
			}
		} catch (err) {
			setError(err instanceof Error ? err.message : 'Failed to fetch properties');
		} finally {
			setLoading(false);
		}
	};

	useEffect(() => {
		fetchProperties();
	}, []);

	const reloadProperties = () => {
		fetchProperties();
	}

	return (
		<>
			{loading && <div>Loading...</div>}
			{error && <div className="error">{error}</div>}

			<div className="row property-list pt-4">
				{
					properties.length ?
						properties.map(property =>
							(<PropertyCard key={property.id} property={property} reload={reloadProperties} />)
						) : (
							<div className="col-12">
								<span>No record found.</span>
							</div>)
				}
			</div>
		</>
	)
}
