import { Link } from "react-router-dom"
import { IPropertyDto } from "../dtos/property/property-dto";
import { PropertyService } from "../services/property.service";

type Props = {
	property: IPropertyDto,
	reload(): void
};

export default function PropertyCard({ property, reload }: Props) {

	const addRemoveFavorite = async (propertyId: number) => {
		const apiResponse = await PropertyService.AddRemoveFavoriteProperty(propertyId);
		if (apiResponse.success) {
			reload();
		} else {
			alert(apiResponse.message);
		}
	}

	return <>
		<div className="col-md-6 col-sm-6 col-xs-12">
			<div className="panel">
				<div className="panel-body">
					<div className="row">
						<div className="col-sm-5">
							<a href="#">
								<img src={property.images[0]} className="img-responsive mb-1" />
							</a>
						</div>
						<div className="col-sm-7">
							<h4 className="title-real-estates row">
								<strong className="col-6">
									<Link to="/properties/1">{property.title}</Link>
								</strong>
								<div className="col-6 f d-flex justify-content-end">
									<span className="me-3">${property.price}</span>
									{
										property.isFavorite ? (
											<i className="bi bi-heart-fill cursor-pointer" onClick={() => addRemoveFavorite(property.id)}></i>
										) : (<i className="bi bi-heart cursor-pointer" onClick={() => addRemoveFavorite(property.id)}></i>)
									}
								</div>
							</h4>
							<hr />
							<p>
								{property.description}
							</p>
							<p className="d-flex justify-content-between">
								<span className="label label-danger">{property.listingType}</span>
								{
									property.bedrooms ? (
										<span className="label label-danger">{property.bedrooms} Beds</span>
									) : ""
								}
								{
									property.bathrooms ? (
										<span className="label label-danger">{property.bathrooms} Baths</span>
									) : ""
								}
							</p>
						</div>
					</div>
				</div>
			</div>
		</div>
	</>
}
